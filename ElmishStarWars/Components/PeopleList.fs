namespace PeopleList

open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 
    type Msg = 
        | PeopleSelectedItemChanged of int option
        | SelectedPeople of People

    type Model = People []



module State = 
    open Types

    let init (people : People []) :(Model * Cmd<Msg>) =
        people, Cmd.none

    let update msg (model: Model) = 
        match msg with 
        | PeopleSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedPeople model.[idx.Value])
        | SelectedPeople a -> model, Cmd.none

module View =
    open Types
    open CommonViews 

    let root (model:Model) dispatch =
        View.ScrollView(
            content = View.StackLayout (
                padding = 20.0,
                children = [
                    if model.Length > 0 then
                        yield View.Label(text = sprintf "no of people : %A" model.Length)
                        yield View.ListView(
                            items = [ 
                               for i in model do 
                                   yield View.Label i.Name
                                   ],
                            itemSelected=(fun idx -> dispatch (PeopleSelectedItemChanged idx)),
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    else yield loadingView "Press to Load People. And press again to load more People"
                ]
            )
        )
