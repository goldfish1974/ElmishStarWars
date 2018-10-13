namespace Person
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = 
        | ShowFilms of string []
        | ShowSpecies of string []
        | ShowVehicles of string []
        | ShowStarships of string []
    type Model = People

module State =
    open Types
    let init (p : People) = 
        p, Cmd.none

    let update msg model = model, Cmd.none //As all the messages are manage from parent.

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
                        yield View.Button(text = "Show Films", command = (fun _ -> ShowFilms model.Films |> dispatch))
                        yield View.Button(text = "Show Species", command = (fun _ -> ShowSpecies model.Species |> dispatch))
                        yield View.Button(text = "Show Vehicles", command = (fun _ -> ShowVehicles model.Vehicles |> dispatch))
                        yield View.Button(text = "Show StarShips", command = (fun _ -> ShowStarships model.Starships |> dispatch))
                    ]
            )).HasNavigationBar(true).HasBackButton(true)