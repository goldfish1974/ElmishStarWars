namespace SpeciesMore
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = 
        | SpeciesListMsg of SpeciesList.Types.Msg
        | LoadSpecies
        | FetchSpecies of Species
        | FetchError of exn

    type Model = {
        Links : string []
        Species : Species []
    }

module State =
    open Types
    open Helpers
    let init (s : string []) = 
        {
            Links = s; Species = [||]
        }, Cmd.ofMsg LoadSpecies

    let update msg model = 
        match msg with 
        | SpeciesListMsg msg ->
            let (a,aCmd) = SpeciesList.State.update msg model.Species
            model, Cmd.map SpeciesListMsg aCmd
        | LoadSpecies -> 
            let fCmds = model.Links |> Array.map getData |> Array.map (fun x -> getCmd x FetchSpecies FetchError)
            model, Cmd.batch fCmds
        | FetchSpecies r -> 
            {model with Species = Array.append model.Species [|r|]}, Cmd.none
        | FetchError exn -> model, Cmd.none

module View =
    open Types
    let root (model: Model) dispatch =
        View.ContentPage(
            title = "Species",
            content =
                View.StackLayout(
                    children = [
                        SpeciesList.View.root model.Species (SpeciesListMsg >> dispatch)
                    ]
                )
            ).HasNavigationBar(true).HasBackButton(true)