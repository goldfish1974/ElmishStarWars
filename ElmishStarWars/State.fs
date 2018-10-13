module State

open Global
open Types
open Fabulous.Core
open Fabulous.DynamicViews

let init () = 
    let (a, aCmd) = Application.State.init()
    {
        PageStack = [(ApplicationPage, ApplicationModel a)]
    }, Cmd.map ApplicationMsg aCmd


let removeFirst (items : 'a list) = 
    match items with 
    | [] -> []
    | [_] -> []
    | h::t -> t

let peopleListProc msg model res =
    match msg with
    | PeopleList.Types.Msg.SelectedPerson i ->
     let (a,aCmd) = Person.State.init(i)
     {model with PageStack = (PersonPage,PersonModel a) :: model.PageStack ; }, Cmd.map PersonMsg aCmd
    | _ -> res

let filmListProc msg model res =
    match msg with
    | FilmList.Types.Msg.SelectedFilm i ->
     let (a,aCmd) = Film.State.init(i)
     {model with PageStack = (FilmPage,FilmModel a) :: model.PageStack ; }, Cmd.map FilmMsg aCmd
    | _ -> res

let starshipListProc msg model res =
    match msg with
    | StarshipList.Types.Msg.SelectedStarship i ->
     let (a,aCmd) = Starship.State.init(i)
     {model with PageStack = (StarshipPage,StarshipModel a) :: model.PageStack ; }, Cmd.map StarshipMsg aCmd
    | _ -> res

let vehicleListProc msg model res =
    match msg with
    | VehicleList.Types.Msg.SelectedVehicle i ->
     let (a,aCmd) = Vehicle.State.init(i)
     {model with PageStack = (VehiclePage,VehicleModel a) :: model.PageStack ; }, Cmd.map VehicleMsg aCmd
    | _ -> res

let speciesListProc msg model res =
    match msg with
    | SpeciesList.Types.Msg.SelectedSpecies i ->
     let (a,aCmd) = Species.State.init(i)
     {model with PageStack = (SpeciesPage,SpeciesModel a) :: model.PageStack ; }, Cmd.map SpeciesMsg aCmd
    | _ -> res

let planetsListProc msg model res =
    match msg with
    | PlanetList.Types.Msg.SelectedPlanet i ->
     let (a,aCmd) = Planet.State.init(i)
     {model with PageStack = (PlanetPage,PlanetModel a) :: model.PageStack ; }, Cmd.map PlanetMsg aCmd
    | _ -> res

let update (msg : Msg) (model : Model) = 
    let (_,pageModel) = model.PageStack.Head
    match msg, pageModel with
    | ApplicationMsg msg , ApplicationModel m ->
        let (a, aCmd) = Application.State.update msg m
        let i = (ApplicationPage, ApplicationModel a) :: removeFirst model.PageStack
        let (res, resCmd) = {model with PageStack = i}, Cmd.map ApplicationMsg aCmd

        match msg with
        | Application.Types.PeopleListMsg p -> peopleListProc p model (res, resCmd)
        | Application.Types.FilmListMsg p -> filmListProc p model (res, resCmd)
        | Application.Types.StarshipListMsg p -> starshipListProc p model (res, resCmd)
        | Application.Types.VehicleListMsg p -> vehicleListProc p model (res, resCmd)
        | Application.Types.SpeciesListMsg p -> speciesListProc p model (res, resCmd)
        | Application.Types.PlanetListMsg p -> planetsListProc p model (res, resCmd)
        | _ -> (res,resCmd)
            
    | PersonMsg msg, PersonModel m ->
        let (a, aCmd) = Person.State.update msg m
        let i = (PersonPage, PersonModel a) :: removeFirst model.PageStack
        let (res,resCmd) = {model with PageStack = i}, Cmd.map PersonMsg aCmd

        match msg with
        | Person.Types.Msg.ShowFilms links ->
            let (a,aCmd) = Films.State.init(links)
            {model with PageStack = (FilmsPage,FilmsModel a) :: model.PageStack ; }, Cmd.map FilmsMsg aCmd
        | Person.Types.Msg.ShowSpecies links ->
            let (a,aCmd) = SpeciesMore.State.init(links)
            {model with PageStack = (SpeciesMorePage,SpeciesMoreModel a) :: model.PageStack ; }, Cmd.map SpeciesMoreMsg aCmd
        | Person.Types.Msg.ShowStarships links ->
            let (a,aCmd) = Starships.State.init(links)
            {model with PageStack = (StarshipsPage,StarshipsModel a) :: model.PageStack ; }, Cmd.map StarshipsMsg aCmd
        | Person.Types.Msg.ShowVehicles links ->
            let (a,aCmd) = Vehicles.State.init(links)
            {model with PageStack = (VehiclesPage,VehiclesModel a) :: model.PageStack ; }, Cmd.map VehiclesMsg aCmd


    | FilmMsg msg , FilmModel m ->
        let (a, aCmd) = Film.State.update msg m
        let i = (FilmPage, FilmModel a) :: removeFirst model.PageStack
        {model with PageStack = i}, Cmd.map FilmMsg aCmd
    | StarshipMsg msg, StarshipModel m ->
        let (a, aCmd) = Starship.State.update msg m
        let i = (StarshipPage, StarshipModel a) :: removeFirst model.PageStack
        {model with PageStack = i}, Cmd.map StarshipMsg aCmd
    | VehicleMsg msg, VehicleModel m ->
        let (a, aCmd) = Vehicle.State.update msg m
        let i = (VehiclePage, VehicleModel a) :: removeFirst model.PageStack
        {model with PageStack = i}, Cmd.map VehicleMsg aCmd
    | SpeciesMsg msg, SpeciesModel m ->
        let (a, aCmd) = Species.State.update msg m
        let i = (SpeciesPage, SpeciesModel a) :: removeFirst model.PageStack
        {model with PageStack = i}, Cmd.map SpeciesMsg aCmd
    | PlanetMsg msg, PlanetModel m ->
        let (a, aCmd) = Planet.State.update msg m
        let i = (PlanetPage, PlanetModel a) :: removeFirst model.PageStack
        {model with PageStack = i}, Cmd.map PlanetMsg aCmd
    | FilmsMsg msg, FilmsModel m ->
        let (a, aCmd) = Films.State.update msg m
        let i = (FilmsPage, FilmsModel a) :: removeFirst model.PageStack
        let (res, resCmd) = {model with PageStack = i}, Cmd.map FilmsMsg aCmd

        match msg with 
        | Films.Types.FilmListMsg p -> filmListProc p model (res, resCmd)
        | _ -> (res, resCmd)

    | SpeciesMoreMsg msg, SpeciesMoreModel m ->
        let (a, aCmd) = SpeciesMore.State.update msg m
        let i = (SpeciesMorePage, SpeciesMoreModel a) :: removeFirst model.PageStack
        let (res, resCmd) = {model with PageStack = i}, Cmd.map SpeciesMoreMsg aCmd

        match msg with 
        | SpeciesMore.Types.SpeciesListMsg p -> speciesListProc p model (res, resCmd)
        | _ -> (res, resCmd)

    | VehiclesMsg msg, VehiclesModel m ->
        let (a, aCmd) = Vehicles.State.update msg m
        let i = (VehiclesPage, VehiclesModel a) :: removeFirst model.PageStack
        let (res, resCmd) = {model with PageStack = i}, Cmd.map VehiclesMsg aCmd

        match msg with 
        | Vehicles.Types.VehicleListMsg p -> vehicleListProc p model (res, resCmd)
        | _ -> (res, resCmd)

    | StarshipsMsg msg, StarshipsModel m ->
        let (a, aCmd) = Starships.State.update msg m
        let i = (StarshipPage, StarshipsModel a) :: removeFirst model.PageStack
        let (res, resCmd) = {model with PageStack = i}, Cmd.map StarshipsMsg aCmd

        match msg with 
        | Starships.Types.StarshipListMsg p -> starshipListProc p model (res, resCmd)
        | _ -> (res, resCmd)

    | PagePopped, _ -> {model with PageStack = removeFirst model.PageStack}, Cmd.none
    | _,_ -> failwithf "%A and %A is not valid msg model pair" msg model