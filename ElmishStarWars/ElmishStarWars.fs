// Copyright 2018 Elmish.XamarinForms contributors. See LICENSE.md for license.
namespace ElmishStarWars

open System.Diagnostics
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open RestSharp
open Newtonsoft.Json
open SWApi
open Types
open State
open Global

module App = 

    let view (model: Model) dispatch =

        let root (page:Page) (pageModel:PageModel) =
            //Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Clicked", sprintf "%A You clicked the button" page, "OK") |> ignore
            match page, pageModel with
            | ApplicationPage _ , ApplicationModel m -> Application.View.root m (ApplicationMsg >> dispatch)
            | PersonPage _, PersonModel m -> Person.View.root m (PersonMsg >> dispatch)
            | FilmPage _, FilmModel m -> Film.View.root m (FilmMsg >> dispatch)
            | StarshipPage _, StarshipModel m -> Starship.View.root m (StarshipMsg >> dispatch)
            | VehiclePage _, VehicleModel m -> Vehicle.View.root m (VehicleMsg >> dispatch)
            | SpeciesPage _, SpeciesModel m -> Species.View.root m (SpeciesMsg >> dispatch)
            | PlanetPage _, PlanetModel m -> Planet.View.root m (PlanetMsg >> dispatch)
            | FilmsPage _, FilmsModel m -> Films.View.root m (FilmsMsg >> dispatch)
            | SpeciesMorePage _, SpeciesMoreModel m -> SpeciesMore.View.root m (SpeciesMoreMsg >> dispatch)
            | VehiclesPage _, VehiclesModel m -> Vehicles.View.root m (VehiclesMsg >> dispatch)
            | StarshipsPage _, StarshipsModel m -> Starships.View.root m (StarshipsMsg >> dispatch)
            | _, _ -> failwith "Wrong page model"

        View.NavigationPage(
          pages = 
            [
                for p in List.rev model.PageStack do
                    let (page,m) = p
                    yield root page m
            ],
            popped=(fun args -> dispatch PagePopped),
            barTextColor = Color.Blue

        )
        //root model.CurrentPage model.PageModel

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> Program.runWithDynamicView app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Elmish.XamarinForms/tools.html for further  instructions.
    //
    do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Elmish.XamarinForms/models.html for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif


