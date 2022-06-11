// <copyright file="AStarPathFinding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// With the help of Sebastian Lague's tutorials @ https://github.com/SebLague/Pathfinding/

namespace KnightCrawler.Engine.Models.Behaviours
{
    using System;
    using System.Collections.Generic;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World.Tiles;

    /// <summary>
    /// The movement behaciour for implementing the A* pathfinding algorithm.
    /// </summary>
    public class AStarPathFinding : EntityMovementBehaviour
    {
        private const int WeightDiagonal = 14;
        private const int WeightAcross = 10;

        /// <summary>
        /// The difference in the coordinets from which the entity considers itself to be on the correct coordinate.
        /// </summary>
        private const float SameLineThreshold = 0.1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="AStarPathFinding"/> class.
        /// </summary>
        /// <param name="speed">The speed of the enemy.</param>
        /// <param name="entity">The entity.</param>
        public AStarPathFinding(double speed, Entity entity)
            : base(speed, entity)
        {
            this.Target = GameModel.GetInstance().Player;

            this.ChasingBehaviour = new MindlessChasing(speed, entity);
            this.Entity.CenterTileChanged += this.Entity_CenterTileChanged;
            this.Target.CenterTileChanged += this.Target_CenterTileChanged;
        }

        /// <summary>
        /// Gets or sets the entity the enemy targets.
        /// </summary>
        public Entity Target { get; set; }

        /// <summary>
        /// Gets the tiles the entity needs to follow.
        /// </summary>
        public List<FloorTile> Path { get; private set; }

        /// <summary>
        /// Gets the behaviour that is only used in case the entity is standing at the same tile as its target, but does not touch it.
        /// </summary>
        private MindlessChasing ChasingBehaviour { get; }

        /// <inheritdoc/>
        public override void OnTick(double deltaTime)
        {
            // If the path is null, then there is no path to the player.
            if (this.Path != null)
            {
                // If the path contains more than one items, the target's tile has been reached.
                if (this.Path.Count > 0)
                {
                    double dx = this.Path[0].Rectangle.Left + (this.Path[0].Rectangle.Width / 2) - this.Entity.Hitbox.Center.X;
                    double dy = this.Path[0].Rectangle.Top + (this.Path[0].Rectangle.Height / 2) - this.Entity.Hitbox.Center.Y;

                    dx = (Math.Abs(dx) < SameLineThreshold) ? 0 : dx;
                    dy = (Math.Abs(dy) < SameLineThreshold) ? 0 : dy;

                    if (!this.Entity.Hitbox.Rectangle.IntersectsWith(this.Target.Hitbox.Rectangle))
                    {
                        this.Entity.MoveDirectional(
                        Math.Sign(dx),
                        Math.Sign(dy),
                        (float)this.Speed,
                        deltaTime,
                        false);
                    }
                }

                // If the path is 0 items long, the enemy has to mindlessly chase the player
                else
                {
                    this.ChasingBehaviour.OnTick(deltaTime);
                }
            }
        }

        /// <summary>
        /// Calculates the weighted cost of getting from on node to another if there were no obstacles in the way.
        /// </summary>
        /// <param name="node1">The starting node.</param>
        /// <param name="node2">The target node.</param>
        /// <returns>Returns the calculated cost.</returns>
        private static int GetDistance(AStarNode node1, AStarNode node2)
        {
            int distX = (int)Math.Abs(node1.Tile.Rectangle.X - node2.Tile.Rectangle.X);
            int distY = (int)Math.Abs(node1.Tile.Rectangle.Y - node2.Tile.Rectangle.Y);

            if (distX > distY)
            {
                return (WeightDiagonal * distY) + (WeightAcross * (distX - distY));
            }
            else
            {
                return (WeightDiagonal * distY) + (WeightAcross * (distY - distX));
            }
        }

        /// <summary>
        /// Finds the path to the target.
        /// </summary>
        private void FindPath()
        {
            // Turning tiles into nodes.
            AStarNode[,] allNodes = new AStarNode[Layout.RoomWidth, Layout.RoomHeight];
            for (int x = 0; x < Layout.RoomWidth; x++)
            {
                for (int y = 0; y < Layout.RoomHeight; y++)
                {
                    allNodes[x, y] = new AStarNode(this.Entity.Room.Tiles[x, y]);
                }
            }

            // Getting start and target nodes.
            FloorTile startTile = this.Entity.TileUnderCenter;
            FloorTile targetTile = this.Target.TileUnderCenter;

            // Turning the target and start tiles into nodes
            AStarNode startNode = allNodes[(int)startTile.Rectangle.X, (int)startTile.Rectangle.Y];
            AStarNode targetNode = allNodes[(int)targetTile.Rectangle.X, (int)targetTile.Rectangle.Y];

            // The collection of nodes that are yet to be evaluated
            List<AStarNode> openSet = new List<AStarNode>();

            // Nodes that are already evaluated
            HashSet<AStarNode> closedSet = new HashSet<AStarNode>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                AStarNode currentNode = openSet[0];

                // Finding the lowest FCost in the open set.
                for (int i = 1; i < openSet.Count; i++)
                {
                    // Finding the node with the lowest F cost (closest to the starting node)
                    if (openSet[i].FCost <= currentNode.FCost)
                    {
                        // If the F costs are the same, we compare H costs
                        if (openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                        {
                            currentNode = openSet[i];
                        }
                    }
                }

                // We store the current node as evaluated.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // If the current node is the target node, we have our path.
                if (currentNode == targetNode)
                {
                    this.Path = this.RetracePath(startNode, targetNode);
                    return;
                }

                // Looping through the current node's neughbours.
                foreach (AStarNode neighbour in this.GetNeighbours(currentNode, allNodes))
                {
                    // We ignore tiles that block pathfinding, or have already been evaluated.
                    if (neighbour.Tile.BlocksPathFinding || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    // Calculating the cost to this neighbor.
                    int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);

                    // If the calculated cost is lower than the previous cost (or has never been evaluated)
                    if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        // We save the G cost
                        neighbour.GCost = newMovementCostToNeighbour;

                        // Calculate H cost (distance from target)
                        neighbour.HCost = GetDistance(neighbour, targetNode);

                        // Storing the current node as it's previous node
                        neighbour.ParentNode = currentNode;

                        // Marking the node as evaluated.
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        private void Target_CenterTileChanged(object sender, EventArgs e)
        {
            this.FindPath();
        }

        private void Entity_CenterTileChanged(object sender, EventArgs e)
        {
            // If the entity stepped on a new tile, we check it if was a target tile, and if so, we remove it from the path.
            if (this.Path != null && this.Path.Count > 0 && sender == this.Path[0])
            {
                this.Path.RemoveAt(0);
            }
        }

        /// <summary>
        /// Reads a backwards from nodes.
        /// </summary>
        /// <param name="startNode">First node.</param>
        /// <param name="targetNode">Last node.</param>
        /// <returns>List of tiles.</returns>
        private List<FloorTile> RetracePath(AStarNode startNode, AStarNode targetNode)
        {
            List<FloorTile> tiles = new List<FloorTile>();
            AStarNode current = targetNode;

            while (current != startNode)
            {
                tiles.Add(current.Tile);
                current = current.ParentNode;
            }

            tiles.Reverse();
            return tiles;
        }

        /// <summary>
        /// Gets the list of nodes neighbouring the given node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="nodeMatrix">The nodes to search in.</param>
        /// <returns>List of nodes.</returns>
        private IEnumerable<AStarNode> GetNeighbours(AStarNode node, AStarNode[,] nodeMatrix)
        {
            List<AStarNode> tiles = new List<AStarNode>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // Ignoring center (it's the current node)
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    // The actual x and y coodrinates to check
                    int checkX = (int)node.Tile.Rectangle.X + x;
                    int checkY = (int)node.Tile.Rectangle.Y + y;

                    // Checking against room tile edges
                    if (checkX >= 0 && checkX < Layout.RoomWidth && checkY >= 0 && checkY < Layout.RoomHeight)
                    {
                        // Checking if the diagonal movement would be stopped by any diagonal obstacles.
                        if (!(Math.Abs(x) == 1 &&
                            Math.Abs(y) == 1 &&
                            (nodeMatrix[checkX, (int)node.Tile.Rectangle.Y].Tile.BlocksPathFinding ||
                            nodeMatrix[(int)node.Tile.Rectangle.X, checkY].Tile.BlocksPathFinding)))
                        {
                            yield return nodeMatrix[checkX, checkY];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A node used for the A* pathfinding algorithm.
        /// </summary>
        public class AStarNode
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AStarNode"/> class.
            /// </summary>
            /// <param name="tile">The tile the node represents.</param>
            public AStarNode(FloorTile tile)
            {
                this.Tile = tile;
            }

            /// <summary>
            /// Gets or sets the distance from the starting tile.
            /// </summary>
            internal int GCost { get; set; }

            /// <summary>
            /// Gets or sets the distance from the target tile.
            /// </summary>
            internal int HCost { get; set; }

            /// <summary>
            /// Gets or sets the node the path comes here from.
            /// </summary>
            internal AStarNode ParentNode { get; set; }

            /// <summary>
            /// Gets the distance from the starting node.
            /// </summary>
            internal int FCost => this.GCost + this.HCost;

            /// <summary>
            /// Gets the tile the node represents.
            /// </summary>
            internal FloorTile Tile { get; }

            /// <inheritdoc/>
            public override string ToString()
            {
                return $"{this.Tile.Rectangle.X} {this.Tile.Rectangle.Y} - {this.Tile.GetType()}";
            }
        }
    }
}