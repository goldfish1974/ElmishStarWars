namespace Vehicles
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = 
        | VehicleListMsg of VehicleList.Types.Msg
        | LoadVehicles
        | FetchVehicle of Vehicle
        | FetchError of exn

    type Model = {
        Links : string []
        Vehicles : Vehicle []
    }

module State =
    open Types
    open Helpers
    let init (v : string []) = 
        {
            Links = v; Vehicles = [||]
        }, Cmd.ofMsg LoadVehicles

    let update msg model = 
        match msg with 
        | VehicleListMsg msg ->
            let (a,aCmd) = VehicleList.State.update msg model.Vehicles
            model, Cmd.map VehicleListMsg aCmd
        | LoadVehicles -> 
            let fCmds = model.Links |> Array.map getData |> Array.map (fun x -> getCmd x FetchVehicle FetchError)
            model, Cmd.batch fCmds
        | FetchVehicle r -> 
            {model with Vehicles = Array.append model.Vehicles [|r|]}, Cmd.none
        | FetchError exn -> model, Cmd.none

module View =
    open Types
    let root (model: Model) dispatch =
        View.ContentPage(
            title = "Vehicles",
            content =
                View.StackLayout(
                    children = [
                        VehicleList.View.root model.Vehicles (VehicleListMsg >> dispatch)
                    ]
                )
            ).HasNavigationBar(true).HasBackButton(true)