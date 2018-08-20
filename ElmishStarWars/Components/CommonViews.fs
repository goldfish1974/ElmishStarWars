module CommonViews
    open Elmish.XamarinForms
    open Elmish.XamarinForms.DynamicViews
    open Xamarin.Forms
    let loadingView (msg : string) =
        View.ContentView(
            content = View.Label (text = msg, horizontalOptions = LayoutOptions.CenterAndExpand)
        )