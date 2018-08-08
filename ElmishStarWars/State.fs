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

let update (msg : Msg) (model : Model) = 
    let (_,pageModel) = model.PageStack.Head
    match msg, pageModel with
    | ApplicationMsg msg , ApplicationModel m ->
        match msg with
        | Application.Types.SelectedPeople p ->
            let (a,aCmd) = People.State.init(p)
            {model with PageStack = (PeoplePage,PeopleModel a) :: model.PageStack ; }, Cmd.map PeopleMsg aCmd
        | Application.Types.SelectedFilm p ->
            let (a,aCmd) = Film.State.init(p)
            {model with PageStack = (FilmPage,FilmModel a) :: model.PageStack ; }, Cmd.map PeopleMsg aCmd
        | Application.Types.SelectedStarship p ->
            let (a,aCmd) = Starship.State.init(p)
            {model with PageStack = (StarshipPage,StarshipModel a) :: model.PageStack ; }, Cmd.map PeopleMsg aCmd
        | Application.Types.SelectedVehicle p ->
            let (a,aCmd) = Vehicle.State.init(p)
            {model with PageStack = (VehiclePage,VehicleModel a) :: model.PageStack ; }, Cmd.map PeopleMsg aCmd
        | Application.Types.SelectedSpecies p ->
            let (a,aCmd) = Species.State.init(p)
            {model with PageStack = (SpeciesPage,SpeciesModel a) :: model.PageStack ; }, Cmd.map PeopleMsg aCmd
        | Application.Types.SelectedPlanet p ->
            let (a,aCmd) = Planet.State.init(p)
            {model with PageStack = (PlanetPage,PlanetModel a) :: model.PageStack ; }, Cmd.map PeopleMsg aCmd
        | _ ->
            let (a, aCmd) = Application.State.update msg m
            let i = (ApplicationPage, ApplicationModel a) :: removeFirst model.PageStack
            {model with PageStack = i}, Cmd.map ApplicationMsg aCmd
    | PeopleMsg msg, PeopleModel m ->
        let (a, aCmd) = People.State.update msg m
        let i = (PeoplePage, PeopleModel a) :: removeFirst model.PageStack
        {model with PageStack = i}, Cmd.map PeopleMsg aCmd
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
    | PagePopped, _ -> {model with PageStack = removeFirst model.PageStack}, Cmd.none
    | _,_ -> failwithf "%A and %A is not valid msg model pair" msg model