module CommonViews
    open Fabulous.Core
    open Fabulous.DynamicViews
    open Xamarin.Forms
    let loadingView (msg : string) =
        View.ContentView(
            content = View.Label (text = msg, horizontalOptions = LayoutOptions.CenterAndExpand)
        )