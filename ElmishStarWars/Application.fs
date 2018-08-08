namespace Application
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System

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
        | PeopleSelectedItemChanged of int option
        | FilmSelectedItemChanged of int option
        | StarshipSelectedItemChanged of int option
        | VehicleSelectedItemChanged of int option 
        | SpeciesSelectedItemChanged of int option 
        | PlanetSelectedItemChanged of int option
        | SelectedPeople of People
        | SelectedFilm of Film
        | SelectedStarship of Starship
        | SelectedVehicle of Vehicle
        | SelectedSpecies of Species
        | SelectedPlanet of Planet

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


    let loopedReq url fetchItems error =
        if (String.IsNullOrEmpty url) 
        then 
            Cmd.none 
        else
            getCmd (getData url) fetchItems error

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
        | FetchPeople r -> 
            let pCmd = loopedReq r.Next FetchPeople FetchError
            let p = model.People |> Array.append r.Results
            {model with People = p}, Cmd.none //pCmd
        | FetchFilm r -> 
            let fCmd = loopedReq r.Next FetchFilm FetchError
            let f = model.Films |> Array.append r.Results
            {model with Films = f}, Cmd.none //fCmd
        | FetchStarship r -> 
            let sCmd = loopedReq r.Next FetchStarship FetchError
            let s = model.Starships |> Array.append r.Results
            {model with Starships = s}, Cmd.none //sCmd
        | FetchVehicle r -> 
            let vCmd = loopedReq r.Next FetchVehicle FetchError
            let v = model.Vehicles |> Array.append r.Results
            {model with Vehicles = v}, Cmd.none //vCmd
        | FetchSpecies r -> 
            let sCmd = loopedReq r.Next FetchSpecies FetchError
            let s = model.Specieses |> Array.append r.Results
            {model with Specieses = s}, Cmd.none //sCmd
        | FetchPlanet r -> 
            let pCmd = loopedReq r.Next FetchPlanet FetchError
            let p = model.Planets |> Array.append r.Results
            {model with Planets = p}, Cmd.none //pCmd
        | FetchError exn -> model, Cmd.none
        | PeopleSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedPeople model.People.[idx.Value])
        | FilmSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedFilm model.Films.[idx.Value])
        | StarshipSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedStarship model.Starships.[idx.Value])
        | VehicleSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedVehicle model.Vehicles.[idx.Value])
        | SpeciesSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedSpecies model.Specieses.[idx.Value])
        | PlanetSelectedItemChanged idx -> model, Cmd.ofMsg (SelectedPlanet model.Planets.[idx.Value])
        | SelectedPeople a -> model, Cmd.none
        | SelectedFilm a -> model, Cmd.none
        | SelectedStarship a -> model, Cmd.none
        | SelectedVehicle a -> model, Cmd.none
        | SelectedSpecies a -> model, Cmd.none
        | SelectedPlanet a -> model, Cmd.none

module View =
    open Types

    let loadingView =
        View.ContentView(
            content = View.Label (text = "Loading", horizontalOptions = LayoutOptions.CenterAndExpand)
        )

    let people model dispatch = 
        View.ContentPage(
            title = "People",
            content =
                View.ScrollView(
                    content = View.StackLayout (
                        padding = 20.0,
                        children = [
                            if model.People.Length > 0 then
                                yield View.Label(text = sprintf "no of people : %A" model.People.Length)
                                yield View.ListView(
                                    items = [ 
                                       for i in model.People do 
                                           yield View.Label i.Name
                                           ],
                                    itemSelected=(fun idx -> dispatch (PeopleSelectedItemChanged idx)),
                                    horizontalOptions=LayoutOptions.CenterAndExpand)
                            else yield loadingView
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
                        if model.Films.Length > 0 then
                            yield View.Label(text = sprintf "no of films : %A" model.Films.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model.Films do 
                                       yield View.Label i.Title
                                       ],
                                itemSelected=(fun idx -> dispatch (FilmSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView
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
                        if model.Starships.Length > 0 then
                            yield View.Label(text = sprintf "no of starships : %A" model.Starships.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model.Starships do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (StarshipSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView
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
                        if model.Vehicles.Length > 0 then
                            yield View.Label(text = sprintf "no of vehicles : %A" model.Vehicles.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model.Vehicles do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (VehicleSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView
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
                        if model.Specieses.Length > 0 then
                            yield View.Label(text = sprintf "no of species : %A" model.Specieses.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model.Specieses do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (SpeciesSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView
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
                        if model.Planets.Length > 0 then
                            yield View.Label(text = sprintf "no of planets : %A" model.Planets.Length)
                            yield View.ListView(
                               items = [ 
                                   for i in model.Planets do 
                                       yield View.Label i.Name
                                       ],
                                itemSelected=(fun idx -> dispatch (PlanetSelectedItemChanged idx)),
                                horizontalOptions=LayoutOptions.CenterAndExpand)
                        else yield loadingView
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