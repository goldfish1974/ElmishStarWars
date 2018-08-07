module State

open Global
open Types
open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews

let init () = 
    let (a, aCmd) = Application.State.init()
    {
        CurrentPage = ApplicationPage
        PageModel = ApplicationModel a
    }, Cmd.map ApplicationMsg aCmd


let update (msg : Msg) (model : Model) = 
    match msg, model.PageModel with
    | ApplicationMsg msg , ApplicationModel m ->
        let (a, aCmd) = Application.State.update msg m
        {model with PageModel = ApplicationModel a}, Cmd.map ApplicationMsg aCmd
    | PeopleMsg msg, PeopleModel m ->
        let (a, aCmd) = People.State.update msg m
        {model with PageModel = PeopleModel a}, Cmd.map PeopleMsg aCmd
    | FilmMsg msg , FilmModel m ->
        let (a, aCmd) = Film.State.update msg m
        {model with PageModel = FilmModel a}, Cmd.map FilmMsg aCmd
    | StarshipMsg msg, StarshipModel m ->
        let (a, aCmd) = Starship.State.update msg m
        {model with PageModel = StarshipModel a}, Cmd.map StarshipMsg aCmd
    | VehicleMsg msg, VehicleModel m ->
        let (a, aCmd) = Vehicle.State.update msg m
        {model with PageModel = VehicleModel a}, Cmd.map VehicleMsg aCmd
    | SpeciesMsg msg, SpeciesModel m ->
        let (a, aCmd) = Species.State.update msg m
        {model with PageModel = SpeciesModel a}, Cmd.map SpeciesMsg aCmd
    | PlanetMsg msg, PlanetModel m ->
        let (a, aCmd) = Planet.State.update msg m
        {model with PageModel = PlanetModel a}, Cmd.map PlanetMsg aCmd
    | _,_ -> failwithf "%A and %A is not valid msg model pair" msg model