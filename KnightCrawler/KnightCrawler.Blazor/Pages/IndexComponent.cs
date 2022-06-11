using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Blazor.Extensions;

namespace KnightCrawler.Blazor.Pages
{
    public class IndexComponent : ComponentBase
    {
        protected BECanvasComponent _canvasReference;


        [Inject]
        internal IJSRuntime JSRuntime { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var tempdata = new byte[6] { 1, 255, 255, 0, 0, 0 };

            if (firstRender)
            {

                var dotNetReference = DotNetObjectReference.Create(this);


                await JSRuntime.InvokeVoidAsync("anim.loadTexture", "assets/sprite.png");


                /// Hook animationloop
                await JSRuntime.InvokeVoidAsync("anim.start", dotNetReference);

                /// Hook MouseEvent on the client                
                await JSRuntime.InvokeVoidAsync("hookMouseEvents", dotNetReference);
            }
        }

        [JSInvokable("eventMouseDown")]
        public async void eventMouseDown(int x, int y)
        {
            //await gameInstance.DoSpecial(x, y);
        }

        [JSInvokable("eventMouseUp")]
        public void eventMouseUp(int x, int y)
        {
            //            await gameInstance.DoSpecial(x, y);
        }


        [JSInvokable("eventRequestAnimationFrame")]
        public async void eventRequestAnimationFrame(int x, int y)
        {

        }
    }
}
