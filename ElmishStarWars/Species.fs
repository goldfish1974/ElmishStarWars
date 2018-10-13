﻿namespace Species
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open SWApi

module Types = 
    type Msg = None
    type Model = Species

module State =
    let init (s: Species) = s, Cmd.none

    let update msg model = model, Cmd.none

module View =
    open Types
    let root (model: Model) dispatch = 
        View.ContentPage(
            content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
                children = [
                        yield View.Label(text = model.Name, horizontalOptions = LayoutOptions.CenterAndExpand)
                    ]
            )).HasNavigationBar(true).HasBackButton(true)