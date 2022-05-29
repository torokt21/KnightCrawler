// <copyright file="JsonScoreRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Repository
{
    using System.Collections.Generic;
    using System.IO;
    using KnightCrawler.Data;
    using Newtonsoft.Json;

    /// <summary>
    /// A repository that manages the scores of players.
    /// </summary>
    public class JsonScoreRepository : IScoreRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonScoreRepository"/> class.
        /// </summary>
        /// <param name="filePath">The path of the json file.</param>
        public JsonScoreRepository()
        {
            this.FilePath = "scores.json";

            if (!File.Exists(this.FilePath))
            {
                File.Create(this.FilePath);
            }
        }

        private string FilePath { get; set; }

        /// <inheritdoc/>
        public List<PlayerScore> GetAll()
        {
            using (StreamReader reader = new StreamReader(this.FilePath))
            {
                List<PlayerScore> scores = JsonConvert.DeserializeObject<List<PlayerScore>>(reader.ReadToEnd()) ?? new List<PlayerScore>();
                return scores;
            }
        }

        /// <inheritdoc/>
        public void InsertNew(string name, int score)
        {
            PlayerScore newScore = new PlayerScore()
            {
                PlayerName = name,
                Score = score,
            };
            if (newScore.PlayerName != null)
            {
                List<PlayerScore> scores = this.GetAll();
                scores.Add(newScore);
                this.SaveScores(scores);
            }
            else
            {
                return;
            }
        }

        private void SaveScores(List<PlayerScore> scores)
        {
            using (StreamWriter writer = new StreamWriter(this.FilePath))
            {
                writer.Write(JsonConvert.SerializeObject(scores, Formatting.Indented));
            }
        }
    }
}