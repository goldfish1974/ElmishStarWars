namespace VehicleList

open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 

    type Msg = 
        | VehicleSelectedItemChanged of int option
        | SelectedVehicle of Vehicle

    type Model = Vehicle []

module State = 
    open Types

    let init (vehicles : Vehicle []) :(Model * Cmd<Msg>) =
        vehicles, Cmd.none

    let update msg (model:Model) = 
        match msg with
        | VehicleSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedVehicle model.[idx.Value])
        | SelectedVehicle a -> model, Cmd.none

module View = 
    open Types
    open CommonViews

    let root (model:Model) dispatch =
        View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        if model.Length > 0 then
                            yield View.Label(text = sprintf "no of vehicles : %A" model.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (VehicleSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView "Press to Load Vehicles. And press again to load more Vehicles"
                    ]
                )
            )
