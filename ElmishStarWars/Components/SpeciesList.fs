namespace SpeciesList

open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 

    type Msg = 
        | SpeciesSelectedItemChanged of int option
        | SelectedSpecies of Species

    type Model = Species []

module State = 
    open Types

    let init (specieses : Species []) :(Model * Cmd<Msg>) =
        specieses, Cmd.none

    let update msg (model:Model) = 
        match msg with 
        | SpeciesSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedSpecies model.[idx.Value])
        | SelectedSpecies a -> model, Cmd.none

module View = 
    open Types
    open CommonViews

    let root (model:Model) dispatch =
        View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        if model.Length > 0 then
                            yield View.Label(text = sprintf "no of species : %A" model.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (SpeciesSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView "Press to Load Species. And press again to load more Species"
                    ]
                )
            )
