module State

open Global
open Types
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews

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

let filmListProc msg model res =
    match msg with
    | FilmList.Types.Msg.SelectedFilm i ->
     let (a,aCmd) = Film.State.init(i)
     {model with PageStack = (FilmPage,FilmModel a) :: model.PageStack ; }, Cmd.map FilmMsg aCmd
    | _ -> res

let update (msg : Msg) (model : Model) = 
    let (_,pageModel) = model.PageStack.Head
    match msg, pageModel with
    | ApplicationMsg msg , ApplicationModel m ->
        let (a, aCmd) = Application.State.update msg m
        let i = (ApplicationPage, ApplicationModel a) :: removeFirst model.PageStack
        let (res, resCmd) = {model with PageStack = i}, Cmd.map ApplicationMsg aCmd

        match msg with
        | Application.Types.PeopleListMsg p ->
            match p with
            | PeopleList.Types.Msg.SelectedPeople i -> 
                let (a,aCmd) = Person.State.init(i)
                {model with PageStack = (PeoplePage,PeopleModel a) :: model.PageStack }, Cmd.map PeopleMsg aCmd
            | _ -> (res,resCmd)
        | Application.Types.FilmListMsg p -> filmListProc p model (res, resCmd)
        | Application.Types.StarshipListMsg p ->
            match p with
            | StarshipList.Types.Msg.SelectedStarship i ->
                let (a,aCmd) = Starship.State.init(i)
                {model with PageStack = (StarshipPage,StarshipModel a) :: model.PageStack ; }, Cmd.map StarshipMsg aCmd
            | _ -> (res,resCmd)
        | Application.Types.VehicleListMsg p ->
            match p with 
            | VehicleList.Types.Msg.SelectedVehicle i ->
                let (a,aCmd) = Vehicle.State.init(i)
                {model with PageStack = (VehiclePage,VehicleModel a) :: model.PageStack ; }, Cmd.map VehicleMsg aCmd
            | _ -> (res,resCmd)
        | Application.Types.SpeciesListMsg p ->
            match p with 
            | SpeciesList.Types.Msg.SelectedSpecies i ->
                let (a,aCmd) = Species.State.init(i)
                {model with PageStack = (SpeciesPage,SpeciesModel a) :: model.PageStack ; }, Cmd.map SpeciesMsg aCmd
            | _ -> (res,resCmd)
        | Application.Types.PlanetListMsg p ->
            match p with
            | PlanetList.Types.Msg.SelectedPlanet i ->
                let (a,aCmd) = Planet.State.init(i)
                {model with PageStack = (PlanetPage,PlanetModel a) :: model.PageStack ; }, Cmd.map PlanetMsg aCmd
            | _ -> (res,resCmd)
        | _ -> (res,resCmd)
            
    | PeopleMsg msg, PeopleModel m ->
        let (a, aCmd) = Person.State.update msg m
        let i = (PeoplePage, PeopleModel a) :: removeFirst model.PageStack
        let (res,resCmd) = {model with PageStack = i}, Cmd.map PeopleMsg aCmd

        match msg with
        | Person.Types.Msg.ShowFilms links ->
            let (a,aCmd) = Films.State.init(links)
            {model with PageStack = (FilmsPage,FilmsModel a) :: model.PageStack ; }, Cmd.map FilmsMsg aCmd
        //| _ ->(res,resCmd)

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

    | PagePopped, _ -> {model with PageStack = removeFirst model.PageStack}, Cmd.none
    | _,_ -> failwithf "%A and %A is not valid msg model pair" msg model