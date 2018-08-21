namespace Films
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = 
        | FilmListMsg of FilmList.Types.Msg
        | LoadFilms
        | FetchFilm of Film
        | FetchError of exn

    type Model = {
        Links : string []
        Films : Film []
    }

module State =
    open Types
    open Helpers
    let init (f : string []) = 
        {
            Links = f; Films = [||]
        }, Cmd.ofMsg LoadFilms

    let update msg model = 
        match msg with 
        | FilmListMsg msg ->
            let (a,aCmd) = FilmList.State.update msg model.Films
            model, Cmd.map FilmListMsg aCmd
        | LoadFilms -> 
            let fCmds = model.Links |> Array.map getData |> Array.map (fun x -> getCmd x FetchFilm FetchError)
            model, Cmd.batch fCmds
        | FetchFilm r -> 
            {model with Films = Array.append model.Films [|r|]}, Cmd.none
        | FetchError exn -> model, Cmd.none

module View =
    open Types
    let root (model: Model) dispatch =
        View.ContentPage(
            title = "Films",
            content =
                View.StackLayout(
                    children = [
                        FilmList.View.root model.Films (FilmListMsg >> dispatch)
                    ]
                )
            ).HasNavigationBar(true).HasBackButton(true)