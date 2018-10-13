module Helpers
    open RestSharp
    open Fabulous.Core
    open Fabulous.DynamicViews
    open Newtonsoft.Json


    let getData (url:string)=
        async {
            do! Async.SwitchToThreadPool()
            let client = new RestClient(url)
            let req = RestRequest(Method.GET)
            return! Async.Catch (client.ExecuteTaskAsync (req) |> Async.AwaitTask)
        }

    let getCmd (res : Async<Choice<IRestResponse,exn>>) success failure = 
       async {
           let! data = res 
           match data with 
           | Choice1Of2 r -> return success (JsonConvert.DeserializeObject<_> r.Content)
           | Choice2Of2 exn -> return failure exn
       } |> Cmd.ofAsyncMsg