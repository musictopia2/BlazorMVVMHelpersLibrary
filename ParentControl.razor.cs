namespace BlazorMVVMHelpersLibrary;
public partial class ParentControl<VM> : IBlazorParent<VM>, IDisposable
    where VM : ScreenViewModel
{
    private readonly IEventAggregator _aggregator;
    public ParentControl()
    {
        if (gg1.Aggregator is null)
        {
            gg1.SetUpDefaultAggregators(); //this will set up defaults
        }
        _aggregator = gg1.Aggregator!;
        Subscribe();
    }
    public VM? DataContext { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    private partial void Subscribe();
    private partial void Unsubscribe();
    protected override void OnInitialized()
    {
        _aggregator.Ask(typeof(VM));
    }
    void IHandle<OpenEventModel>.Handle(OpenEventModel message)
    {
        DataContext = message.ViewModelUsed as VM;
        InvokeAsync(StateHasChanged);
    }
    void IHandle<CloseEventModel>.Handle(CloseEventModel message)
    {
        DataContext = null;
        InvokeAsync(StateHasChanged);
    }
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    {
        Unsubscribe();
    }
}