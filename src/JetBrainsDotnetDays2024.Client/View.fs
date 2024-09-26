module JetBrainsDotnetDays2024.Client.View

open Feliz
open Router
open Elmish
open Feliz.UseElmish

type private Msg =
    | UrlChanged of Page

type private State = {
    Page : Page
}

let private init () =
    let nextPage = Router.currentPath() |> Page.parseFromUrlSegments
    { Page = nextPage }, Cmd.navigatePage nextPage

let private update (msg:Msg) (state:State) : State * Cmd<Msg> =
    match msg with
    | UrlChanged page -> { state with Page = page }, Cmd.none

[<ReactComponent>]
let AppView () =
    let state,dispatch = React.useElmish(init, update)
    let navigation =
        Html.div [
            Html.a("Home", Page.Index)
            Html.span " | "
            Html.a("Fable Example", Page.FableExample)
            Html.span " | "
            Html.a("Elmish Example", Page.ElmishExample)
        ]
    let render =
        match state.Page with
        | Page.Index -> Pages.Index.IndexView ()
        | Page.FableExample -> Pages.FableExample.FableExampleView ()
        | Page.ElmishExample -> Pages.ElmishExample.ElmishExampleView ()
    React.router [
        router.pathMode
        router.onUrlChanged (Page.parseFromUrlSegments >> UrlChanged >> dispatch)
        router.children [
            Html.divClassed "h-screen" [
                navigation
                render
            ]
        ]
    ]