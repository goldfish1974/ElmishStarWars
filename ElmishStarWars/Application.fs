namespace Application
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open SWApi
open Helpers
open System
open Newtonsoft.Json

module Types = 
    type Msg = 
        | FetchRoot of Root
        | LoadPeople
        | FetchPeople of Resp<People>
        | LoadFilms
        | FetchFilm of Resp<Film>
        | LoadStarships
        | FetchStarship of Resp<Starship>
        | LoadVehicles
        | FetchVehicle of Resp<Vehicle>
        | LoadSpecies
        | FetchSpecies of Resp<Species>
        | LoadPlanets
        | FetchPlanet of Resp<Planet>
        | FetchError of exn
        | PeopleListMsg of PeopleList.Types.Msg
        | FilmListMsg of FilmList.Types.Msg
        | StarshipListMsg of StarshipList.Types.Msg
        | VehicleListMsg of VehicleList.Types.Msg
        | SpeciesListMsg of SpeciesList.Types.Msg
        | PlanetListMsg of PlanetList.Types.Msg

    type Model = {
        Root : Root option
        PeopleRes : Resp<People> option
        People : PeopleList.Types.Model
        FilmRes : Resp<Film> option
        Films : FilmList.Types.Model
        StarshipRes : Resp<Starship> option
        Starships : StarshipList.Types.Model
        VehicleRes : Resp<Vehicle> option
        Vehicles : VehicleList.Types.Model
        SpeciesRes : Resp<Species> option
        Species : SpeciesList.Types.Model
        PlanetRes : Resp<Planet> option
        Planets : PlanetList.Types.Model
    }

module State =
    open Types

    let init () = 
        let resCmd = getCmd (getData rootUrl) FetchRoot FetchError
        {
            Root = None
            PeopleRes = None
            People = [||]
            FilmRes = None
            Films = [||]
            StarshipRes = None
            Starships = [||]
            VehicleRes = None
            Vehicles = [||]
            SpeciesRes = None
            Species = [||]
            PlanetRes = None
            Planets = [||]
        }, resCmd

    let fetchDataFromResp baseUrl (r : Resp<'a> option) success error= 
        let fetchDataIfPossible r success error = 
            if (String.IsNullOrEmpty r.Next) then //No next so don't do anything
                Cmd.none
            else getCmd (getData r.Next) success error //Pull the next data

        match r with 
        | None -> 
            getCmd (getData baseUrl) success error // we don't have object so pull it from root
        | Some r ->
            fetchDataIfPossible r success error

    let update msg model = 
        match msg with 
        | FetchRoot r -> {model with Root = Some r}, Cmd.none
        | LoadPeople -> 
            model, fetchDataFromResp model.Root.Value.People model.PeopleRes FetchPeople FetchError
        | FetchPeople r -> 
            let (a,aCmd) = PeopleList.State.init r.Results
            {model with People = Array.append model.People a; PeopleRes = Some r}, Cmd.map PeopleListMsg aCmd
        | LoadFilms ->
            model, fetchDataFromResp model.Root.Value.Films model.FilmRes FetchFilm FetchError
        | FetchFilm r ->
            let (a,aCmd) = FilmList.State.init r.Results
            {model with Films = Array.append model.Films a; FilmRes = Some r}, Cmd.map FilmListMsg aCmd
        | LoadStarships ->
            model, fetchDataFromResp model.Root.Value.Starships model.StarshipRes FetchStarship FetchError
        | FetchStarship r -> 
            let (a,aCmd) = StarshipList.State.init r.Results
            {model with Starships = Array.append model.Starships a; StarshipRes = Some r}, Cmd.map StarshipListMsg aCmd
        | LoadVehicles ->
            model, fetchDataFromResp model.Root.Value.Vehicles model.VehicleRes FetchVehicle FetchError
        | FetchVehicle r -> 
            let (a,aCmd) = VehicleList.State.init r.Results
            {model with Vehicles = Array.append model.Vehicles a; VehicleRes = Some r}, Cmd.map VehicleListMsg aCmd
        | LoadSpecies ->
            model, fetchDataFromResp model.Root.Value.Species model.SpeciesRes FetchSpecies FetchError
        | FetchSpecies r -> 
            let (a,aCmd) = SpeciesList.State.init r.Results
            {model with Species = Array.append model.Species a; SpeciesRes = Some r}, Cmd.map SpeciesListMsg aCmd
        | LoadPlanets ->
            model, fetchDataFromResp model.Root.Value.Planets model.PlanetRes FetchPlanet FetchError
        | FetchPlanet r -> 
            let (a,aCmd) = PlanetList.State.init r.Results
            {model with Planets = Array.append model.Planets a; PlanetRes = Some r}, Cmd.map PlanetListMsg aCmd
        | FetchError exn -> model, Cmd.none
        | PeopleListMsg msg -> 
            let (a,aCmd) = PeopleList.State.update msg model.People
            model, Cmd.map PeopleListMsg aCmd
        | FilmListMsg msg ->
            let (a,aCmd) = FilmList.State.update msg model.Films
            model, Cmd.map FilmListMsg aCmd
        | StarshipListMsg msg ->
            let (a,aCmd) = StarshipList.State.update msg model.Starships
            model, Cmd.map StarshipListMsg aCmd
        | VehicleListMsg msg ->
            let (a,aCmd) = VehicleList.State.update msg model.Vehicles
            model, Cmd.map VehicleListMsg aCmd
        | SpeciesListMsg msg ->
            let (a,aCmd) = SpeciesList.State.update msg model.Species
            model, Cmd.map SpeciesListMsg aCmd
        | PlanetListMsg msg ->
            let (a,aCmd) = PlanetList.State.update msg model.Planets
            model, Cmd.map PlanetListMsg aCmd

module View =
    open Types
    open CommonViews


    let root (model: Model) dispatch = 
        View.TabbedPage(
                useSafeArea = true,
                children = [
                    yield 
                        View.ContentPage(
                            title = "People",
                            content = 
                                View.StackLayout(
                                    children = [
                                        PeopleList.View.root model.People (PeopleListMsg >> dispatch)
                                        View.Button(text = "Fetch People",command = (fun _ -> LoadPeople |> dispatch))
                                    ]
                                )
                            )
                    yield
                        View.ContentPage(
                            title = "Film",
                            content =
                                View.StackLayout(
                                    children = [
                                        FilmList.View.root model.Films (FilmListMsg >> dispatch)
                                        View.Button(text = "Fetch Films",command = (fun _ -> LoadFilms |> dispatch))
                                    ]
                                )
                            )
                    yield
                        View.ContentPage(
                            title = "Starship",
                            content = 
                                View.StackLayout(
                                    children = [
                                        StarshipList.View.root model.Starships (StarshipListMsg >> dispatch)
                                        View.Button(text = "Fetch Starships",command = (fun _ -> LoadStarships |> dispatch))
                                    ]
                                )
                            )

                    yield
                        View.ContentPage(
                            title = "Vehicle",
                            content = 
                                View.StackLayout(
                                    children = [
                                        VehicleList.View.root model.Vehicles (VehicleListMsg >> dispatch)
                                        View.Button(text = "Fetch Vehicles",command = (fun _ -> LoadVehicles |> dispatch))
                                    ]
                                )
                            )

                    yield
                        View.ContentPage(
                            title = "Species",
                            content = 
                                View.StackLayout(
                                    children = [
                                        SpeciesList.View.root model.Species (SpeciesListMsg >> dispatch)
                                        View.Button(text = "Fetch Species",command = (fun _ -> LoadSpecies |> dispatch))
                                    ]
                                )
                            )

                    yield
                        View.ContentPage(
                            title = "Planet",
                            content = 
                                View.StackLayout(
                                    children = [
                                        PlanetList.View.root model.Planets (PlanetListMsg >> dispatch)
                                        View.Button(text = "Fetch Planes",command = (fun _ -> LoadPlanets |> dispatch))
                                    ]
                                )
                            )

                ]
            ).HasNavigationBar(true).HasBackButton(true)