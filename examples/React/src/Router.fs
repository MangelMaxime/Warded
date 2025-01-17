﻿[<RequireQualifiedAccess>]
module Router

open Elmish.UrlParser
open Elmish.Navigation
open Feliz
open Browser.Dom

[<RequireQualifiedAccess>]
type ReactRoute =
    | ReactOnly
    | CustomView

    interface SharedRouter.IRoute with
        member this.Segments =
            match this with
            | ReactOnly -> "react-only"
            | CustomView -> "custom-view"

type Route = SharedRouter.Route<ReactRoute>

let routeParser: Parser<Route -> Route, Route> =
    oneOf [
        map ReactRoute.ReactOnly (s "react-only")
        map ReactRoute.CustomView (s "custom-view")
    ]
    |> SharedRouter.routeParser

let href (route: Route) = prop.href route.HashPart

let modifyLocation (route: Route) = window.location.href <- route.HashPart

let newUrl (route: Route) = route.HashPart |> Navigation.newUrl
