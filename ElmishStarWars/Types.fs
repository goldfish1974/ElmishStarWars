module Types
open Global

type Msg = 
    | ApplicationMsg of Application.Types.Msg
    | PeopleMsg of Person.Types.Msg
    | FilmMsg of Film.Types.Msg
    | StarshipMsg of Starship.Types.Msg
    | VehicleMsg of Vehicle.Types.Msg
    | SpeciesMsg of Species.Types.Msg
    | PlanetMsg of Planet.Types.Msg
    | FilmsMsg of Films.Types.Msg
    | PagePopped


type PageModel =
    | ApplicationModel of Application.Types.Model
    | PeopleModel of Person.Types.Model
    | FilmModel of Film.Types.Model
    | StarshipModel of Starship.Types.Model
    | VehicleModel of Vehicle.Types.Model
    | SpeciesModel of Species.Types.Model
    | PlanetModel of Planet.Types.Model
    | FilmsModel of Films.Types.Model


type Model = {
        PageStack : (Page * PageModel) list
        //PageModel : PageModel
    }