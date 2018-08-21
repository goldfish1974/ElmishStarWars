namespace Starships
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = 
        | StarshipListMsg of StarshipList.Types.Msg
        | LoadStarships
        | FetchStarship of Starship
        | FetchError of exn

    type Model = {
        Links : string []
        Starships : Starship []
    }

module State =
    open Types
    open Helpers
    let init (f : string []) = 
        {
            Links = f; Starships = [||]
        }, Cmd.ofMsg LoadStarships

    let update msg model = 
        match msg with 
        | StarshipListMsg msg ->
            let (a,aCmd) = StarshipList.State.update msg model.Starships
            model, Cmd.map StarshipListMsg aCmd
        | LoadStarships -> 
            let fCmds = model.Links |> Array.map getData |> Array.map (fun x -> getCmd x FetchStarship FetchError)
            model, Cmd.batch fCmds
        | FetchStarship r -> 
            {model with Starships = Array.append model.Starships [|r|]}, Cmd.none
        | FetchError exn -> model, Cmd.none

module View =
    open Types
    let root (model: Model) dispatch =
        View.ContentPage(
            title = "Starships",
            content =
                View.StackLayout(
                    children = [
                        StarshipList.View.root model.Starships (StarshipListMsg >> dispatch)
                    ]
                )
            ).HasNavigationBar(true).HasBackButton(true)