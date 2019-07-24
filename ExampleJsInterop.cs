using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Tazor {
    public class ExampleJsInterop {
        private readonly IJSRuntime _jsRuntime;

        public ExampleJsInterop(IJSRuntime jsRuntime) {
            _jsRuntime = jsRuntime;
        }

        public Task<string> Prompt(string message) {
            // Implemented in exampleJsInterop.js
            return _jsRuntime.InvokeAsync<string>("exampleJsFunctions.showPrompt", message);
        }
    }
}
