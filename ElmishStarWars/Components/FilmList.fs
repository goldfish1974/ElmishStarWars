namespace FilmList

open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 
    type Msg = 
        | FilmSelectedItemChanged of int option
        | SelectedFilm of Film

    type Model = Film []

module State = 
    open Types

    let init (films : Film []) :(Model * Cmd<Msg>) =
        films, Cmd.none

    let update msg (model:Model) = 
        match msg with
        | FilmSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedFilm model.[idx.Value])
        | SelectedFilm a -> model, Cmd.none
module View = 
    open Types
    open CommonViews

    let root (model:Model) dispatch =
        View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        if model.Length > 0 then
                            yield View.Label(text = sprintf "no of films : %A" model.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model do 
                                       yield View.Label i.Title
                                       ],
                                itemSelected=(fun idx -> dispatch (FilmSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView "Press to Load Films. And press again to load more Films"
                    ]
                )
            )
