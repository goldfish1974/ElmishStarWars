namespace Starship
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms

module Types = 
    type Msg = None
    type Model = string

module State =
    let init () = "", Cmd.none

    let update msg model = "", Cmd.none

module View =
    open Types
    let view (model: Model) dispatch = ""