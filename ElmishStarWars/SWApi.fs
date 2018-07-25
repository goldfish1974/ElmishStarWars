module SWApi 

let [<Literal>] rootUrl = "https://swapi.co/api/"

//This was actually not required as all is very much restful. But still nothing hurt to have it.
let [<Literal>] peopleUrl = rootUrl + "/people/"
let [<Literal>] filmsUrl = rootUrl + "/films/"
let [<Literal>] starshipsUrl = rootUrl + "/starships/"
let [<Literal>] vehiclesUrl = rootUrl + "/vehicles/"
let [<Literal>] speciesUrl = rootUrl + "/species/"
let [<Literal>] planetsUrl = rootUrl + "/planets/"


[<CLIMutable>]
type Root = {
    Films: string
    People: string
    Planets: string
    Species: string
    Starships: string
    Vehicles: string
} 

[<CLIMutable>]
type People = {
    (**
    * An array of urls of film resources that this person has been in.
    *)
    Films: string[]
    (**
    * the ISO 8601 date format of the time that this resource was edited.
    *)
    Edited: string
    (**
    * The height of this person in meters.
    *)
    Height: string
    (**
    * The birth year of this person. BBY (Before the Battle of Yavin) or ABY (After the Battle of Yavin).
    *)
    Birth_Year: string
    (**
    * The hair color of this person.
    *)
    Hair_Color: string
    (**
    * The mass of this person in kilograms.
    *)
    Mass: string
    (**
    * The gender of this person (if known).
    *)
    Gender: string
    (**
    * The url of this resource
    *)
    Url: string
    (**
    * The ISO 8601 date format of the time that this resource was created.
    *)
    Created: string
    (**
    * The eye color of this person.
    *)
    Eye_Color: string
    (**
    * An array of starship resources that this person has piloted
    *)
    Starships: string[]
    (**
    * The skin color of this person.
    *)
    Skin_Color: string
    (**
    * The url of the planet resource that this person was born on.
    *)
    HomeWorld: string
    (**
    * The url of the species resource that this person is.
    *)
    Species: string[]
    (**
    * An array of vehicle resources that this person has piloted
    *)
    Vehicles: string[]
    (**
    * The name of this person.
    *)
    Name: string
}
and [<CLIMutable>] Film = {
    (**
    * The title of this film.
    *)
    Title: string
    (**
    * The release date at original creator country.
    *)
    Release_Date: string
    (**
    * The director of this film.
    *)
    Director: string
    (**
    * The producer(s) of this film.
    *)
    Producer: string
    (**
    * The url of this resource
    *)
    Url: string
    (**
    * The vehicle resources featured within this film.
    *)
    Vehicles: string[]
    (**
    * The starship resources featured within this film.
    *)
    Starships: string[]
    (**
    * The ISO 8601 date format of the time that this resource was created.
    *)
    Created: string
    (**
    * The opening crawl text at the beginning of this film.
    *)
    Opening_Crawl: string
    (**
    * The people resources featured within this film.
    *)
    Characters: string[]
    (**
    * the ISO 8601 date format of the time that this resource was edited.
    *)
    Edited: string
    (**
    * The planet resources featured within this film.
    *)
    Planets: string[]
    (**
    * The episode number of this film.
    *)
    Episode_Id: int
    (**
    * The species resources featured within this film.
    *)
    Species: string[]
}
and [<CLIMutable>] Starship = {
    (**
    * The maximum speed of this starship in atmosphere. n/a if this starship is incapable of atmosphering flight.
    *)
    Max_Atmosphering_Speed: string
    (**
    * The manufacturer of this starship. Comma seperated if more than one.
    *)
    Manufacturer: string
    (**
    * An array of Film URL Resources that this starship has appeared in.
    *)
    Films: string[]
    (**
    * The number of non-essential people this starship can transport.
    *)
    Passengers: string
    (**
    * The ISO 8601 date format of the time that this resource was created.
    *)
    Created: string
    (**
    * the ISO 8601 date format of the time that this resource was edited.
    *)
    Edited: string
    (**
    * The maximum length of time that this starship can provide consumables for its entire crew without having to resupply.
    *)
    Consumables: string
    (**
    * The number of personnel needed to run or pilot this starship.
    *)
    Crew: string
    (**
    * The length of this starship in meters.
    *)
    Length: string
    (**
    * An array of People URL Resources that this starship has been piloted by.
    *)
    Pilots: string[]
    (**
    * The maximum number of kilograms that this starship can transport.
    *)
    Cargo_Capacity: string
    (**
    * The class of this starships hyperdrive.
    *)
    Hyperdrive_Rating: string
    (**
    * The hypermedia URL of this resource.
    *)
    Url: string
    (**
    * The model or official name of this starship. Such as T-65 X-wing or DS-1 Orbital Battle Station.
    *)
    Model: string
    (**
    * The class of this starship, such as Starfighter or Deep Space Mobile Battlestation.
    *)
    Starship_Class: string
    (**
    * The cost of this starship new, in galactic credits.
    *)
    Cost_In_Credits: string
    (**
    * The Maximum number of Megalights this starship can travel in a standard hour. A Megalight is a standard unit of distance and has never been defined before within the Star Wars universe. This figure is only really useful for measuring the difference in speed of starships. We can assume it is similar to AU, the distance between our Sun (Sol) and Earth.
    *)
    MGLT: string
    (**
    * The name of this starship. The common name, such as Death Star.
    *)
    Name: string
}
and [<CLIMutable>] Species = {
    (**
    * The language commonly spoken by this species.
    *)
    Language: string
    (**
    *  An array of Film URL Resources that this species has appeared in.
    *)
    Films: string[]
    (**
    * The ISO 8601 date format of the time that this resource was created.
    *)
    Created: string
    (**
    * The average lifespan of this species in years.
    *)
    Average_Lifespan: string
    (**
    * A comma-seperated string of common eye colors for this species, none if this species does not typically have eyes.
    *)
    Eye_Colors: string
    (**
    * The hypermedia URL of this resource.
    *)
    Url: string
    (**
    * A comma-seperated string of common hair colors for this species, none if this species does not typically have hair.
    *)
    Hair_Colors: string
    (**
    * A comma-seperated string of common skin colors for this species, none if this species does not typically have skin.
    *)
    Skin_Colors: string
    (**
    * The URL of a planet resource, a planet that this species originates from.
    *)
    Homeworld: string
    (**
    * An array of People URL Resources that are a part of this species.
    *)
    People: string[]
    (**
    * The average height of this person in centimeters.
    *)
    Average_Height: string
    (**
    * The designation of this species.
    *)
    Designation: string
    (**
    * The ISO 8601 date format of the time that this resource was edited.
    *)
    edited: string
    (**
    * The classification of this species.
    *)
    classification: string
    (**
    * The name of this species.
    *)
    name: string
}
and [<CLIMutable>] Vehicle = {
    (**
    * The maximum speed of this vehicle in atmosphere.
    *)
    Max_Atmosphering_Speed: string
    (**
    * The manufacturer of this vehicle. Comma seperated if more than one.
    *)
    Manufacturer: string
    (**
    * An array of Film URL Resources that this vehicle has appeared in.
    *)
    Films: string[]
    (**
    * The number of non-essential people this vehicle can transport.
    *)
    Passengers: string
    (**
    * the ISO 8601 date format of the time that this resource was edited.
    *)
    Edited: string
    (**
    * The maximum length of time that this vehicle can provide consumables for its entire crew without having to resupply.
    *)
    Consumables: string
    (**
    * The number of personnel needed to run or pilot this vehicle.
    *)
    Crew: string
    (**
    * The length of this vehicle in meters.
    *)
    Length: string
    (**
    * An array of People URL Resources that this vehicle has been piloted by.
    *)
    Pilots: string[]
    (**
    * The ISO 8601 date format of the time that this resource was created.
    *)
    Created: string
    (**
    * The class of this vehicle, such as Wheeled.
    *)
    Vehicle_Class: string
    (**
    * The hypermedia URL of this resource.
    *)
    Url: string
    (**
    * The model or official name of this vehicle. Such as All Terrain Attack Transport.
    *)
    Model: string
    (**
    * The maximum number of kilograms that this vehicle can transport.
    *)
    Cargo_Capacity: string
    (**
    * The cost of this vehicle new, in galactic credits.
    *)
    Cost_In_Credits: string
    (**
    * The name of this vehicle. The common name, such as Sand Crawler.
    *)
    Name: string
}



module Http =
    open RestSharp
    open Newtonsoft.Json

    let getData<'a> (url:string)=
        async {
            let client = new RestClient(url)
            let req = RestRequest(Method.GET)
            return! Async.Catch (client.ExecuteGetTaskAsync<'a>(req) |> Async.AwaitTask)
        } 
