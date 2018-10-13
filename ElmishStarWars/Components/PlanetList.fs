namespace PlanetList

open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 

    type Msg = 
        | PlanetSelectedItemChanged of int option
        | SelectedPlanet of Planet

    type Model = Planet []

module State = 
    open Types

    let init (planets : Planet []) :(Model * Cmd<Msg>) =
        planets, Cmd.none

    let update msg (model:Model) = 
        match msg with
        | PlanetSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedPlanet model.[idx.Value])
        | SelectedPlanet a -> model, Cmd.none
module View = 
    open Types
    open CommonViews

    let root (model:Model) dispatch =
        View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        if model.Length > 0 then
                            yield View.Label(text = sprintf "no of planets : %A" model.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (PlanetSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView "Press to Load Planets. And press again to load more Planets"
                    ]
                )
            )

