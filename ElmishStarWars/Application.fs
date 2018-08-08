namespace Application
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers

module Types = 
    type Msg = 
        | FetchRoot of Root
        | FetchList
        | FetchPeople of Resp<People>
        | FetchFilm of Resp<Film>
        | FetchStarship of Resp<Starship>
        | FetchVehicle of Resp<Vehicle>
        | FetchSpecies of Resp<Species>
        | FetchPlanet of Resp<Planet>
        | FetchError of exn

    type Model = {
        Root : Root option
        People : People []
        Films : Film  []
        Starships : Starship []
        Vehicles : Vehicle []
        Specieses : Species []
        Planets : Planet []
    }

module State =
    open Types

    let init () = 
        let resCmd = getCmd (getData rootUrl) FetchRoot FetchError
        {
            Root = None
            People = [||]
            Films = [||]
            Starships = [||]
            Vehicles = [||]
            Specieses = [||]
            Planets = [||]
        }, resCmd

    let update msg model = 
        match msg with 
        | FetchRoot r -> {model with Root = Some r}, Cmd.ofMsg FetchList
        | FetchList -> 
            let peopleCmd = getCmd (getData model.Root.Value.People) FetchPeople FetchError
            let filmCmd = getCmd (getData model.Root.Value.Films) FetchFilm FetchError
            let starshipCmd = getCmd (getData model.Root.Value.Starships) FetchStarship FetchError
            let vehicleCmd = getCmd (getData model.Root.Value.Vehicles) FetchVehicle FetchError
            let speciesCmd = getCmd (getData model.Root.Value.Species) FetchSpecies FetchError
            let planetCmd = getCmd (getData model.Root.Value.Planets) FetchPlanet FetchError
            model, Cmd.batch [
                peopleCmd
                filmCmd
                starshipCmd
                vehicleCmd
                speciesCmd
                planetCmd
            ]
        | FetchPeople r -> {model with People = r.Results}, Cmd.none
        | FetchFilm r -> {model with Films = r.Results}, Cmd.none
        | FetchStarship r -> {model with Starships = r.Results}, Cmd.none
        | FetchVehicle r -> {model with Vehicles = r.Results}, Cmd.none
        | FetchSpecies r -> {model with Specieses = r.Results}, Cmd.none
        | FetchPlanet r -> {model with Planets = r.Results}, Cmd.none
        | FetchError exn -> model, Cmd.none

module View =
    open Types


    let people model dispatch = 
        View.ContentPage(
            title = "People",
            content = View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        View.Label(text = sprintf "no of people : %A" model.People.Length)
                        View.ListView(
                           items = [ 
                               for i in model.People do 
                                   yield View.Label i.Name
                                   ],
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    ]
                )
            )
        )

    let film model dispatch = 
        View.ContentPage(
            title = "Film",
            content = View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        View.Label(text = sprintf "no of films : %A" model.Films.Length)
                        View.ListView(
                           items = [ 
                               for i in model.Films do 
                                   yield View.Label i.Title
                                   ],
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    ]
                )
            )
        )

    let starship model dispatch = 
        View.ContentPage(
            title = "Starship",
            content = View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        View.Label(text = sprintf "no of starships : %A" model.Starships.Length)
                        View.ListView(
                           items = [ 
                               for i in model.Starships do 
                                   yield View.Label i.Name
                                   ],
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    ]
                )
            )
        )

    let vehicle model dispatch = 
        View.ContentPage(
            title = "Vehicle",
            content = View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        View.Label(text = sprintf "no of vehicles : %A" model.Vehicles.Length)
                        View.ListView(
                           items = [ 
                               for i in model.Vehicles do 
                                   yield View.Label i.Name
                                   ],
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    ]
                )
            )
        )

    let species model dispatch = 
        View.ContentPage(
            title = "Species",
            content = View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        View.Label(text = sprintf "no of species : %A" model.Specieses.Length)
                        View.ListView(
                           items = [ 
                               for i in model.Specieses do 
                                   yield View.Label i.Name
                                   ],
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    ]
                )
            )
        )

    let planet model dispatch = 
        View.ContentPage(
            title = "Planet",
            content = View.ScrollView(
                content = View.StackLayout (
                    padding = 20.0,
                    children = [
                        View.Label(text = sprintf "no of planets : %A" model.Planets.Length)
                        View.ListView(
                           items = [ 
                               for i in model.Planets do 
                                   yield View.Label i.Name
                                   ],
                            horizontalOptions=LayoutOptions.CenterAndExpand)
                    ]
                )
            )
        )
    
    let root (model: Model) dispatch = 
        View.TabbedPage(
            useSafeArea = true,
            children = [
                yield people model dispatch
                yield film model dispatch
                yield starship model dispatch
                yield vehicle model dispatch
                yield species model dispatch
                yield planet model dispatch
            ]
        ).HasNavigationBar(true).HasBackButton(true)