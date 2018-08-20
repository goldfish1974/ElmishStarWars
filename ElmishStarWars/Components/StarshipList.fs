namespace StarshipList

open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 

    type Msg = 
        | StarshipSelectedItemChanged of int option
        | SelectedStarship of Starship

    type Model = Starship []

module State = 
    open Types

    let init (starships : Starship []) :(Model * Cmd<Msg>) =
        starships, Cmd.none

    let update msg (model:Model) = 
        match msg with 
        | StarshipSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedStarship model.[idx.Value])
        | SelectedStarship a -> model, Cmd.none

module View = 
    open Types
    open CommonViews

    let root (model:Model) dispatch =
       View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        if model.Length > 0 then
                            yield View.Label(text = sprintf "no of starships : %A" model.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (StarshipSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView "Press to Load Starships. And press again to load more Starships"
                    ]
                )
            )
