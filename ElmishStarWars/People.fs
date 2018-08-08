namespace People
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = None
    type Model = People

module State =
    let init (p : People) = 
        p, Cmd.none

    let update msg model = model, Cmd.none

module View =
    open Types
    let root (model: Model) dispatch =
        View.ContentPage(
            content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
                children = [
                        yield View.Label(text = model.Name, horizontalOptions = LayoutOptions.CenterAndExpand)
                        yield View.Label(text = model.Birth_Year, horizontalOptions = LayoutOptions.CenterAndExpand)
                        yield View.Label(text = model.Gender, horizontalOptions = LayoutOptions.CenterAndExpand)
                        yield View.Label(text = model.Hair_Color, horizontalOptions = LayoutOptions.CenterAndExpand)
                    ]
            )).HasNavigationBar(true).HasBackButton(true)