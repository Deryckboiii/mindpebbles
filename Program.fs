open System
open System.Text.Json
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http
open Giraffe
open MindPebbles.Models
open MindPebbles.Views
open Giraffe.ViewEngine

let rnd = System.Random()

let getRandomFromList (lst: Thought list) =
    let arr = List.toArray lst
    let index = rnd.Next(arr.Length)
    arr.[index].Text

let getRandomColor () =
    let colors = [| "#fef6e4"; "#e0f7fa"; "#e6e6fa"; "#ffe4e1"; "#f0fff0"; "#f0f8ff"; "#fffacd"; "#f5f5dc"; "#faf0e6" |]
    colors.[rnd.Next(colors.Length)]

let homeHandler : HttpHandler =
    fun next ctx ->
        let quote = getRandomFromList (allThoughts())
        let bgColor = getRandomColor()
        htmlView (homeView quote bgColor) next ctx

let categoryHandler (category: string) : HttpHandler =
    fun next ctx ->
        let filtered = getThoughtsByCategory category
        let quote =
            if List.isEmpty filtered then "No pebbles in this category."
            else getRandomFromList filtered
        let bgColor = getRandomColor()
        htmlView (homeView quote bgColor) next ctx

let jsonSettings = JsonSerializerOptions(WriteIndented = true)

let apiAllHandler : HttpHandler =
    fun next ctx ->
        let data = allThoughts()
        json data next ctx

let apiRandomHandler : HttpHandler =
    fun next ctx ->
        let quote = getRandomFromList (allThoughts())
        json {| text = quote |} next ctx

let statsHandler : HttpHandler =
    fun next ctx ->
        let (total, custom, grouped) = stats()
        let content =
            div [] [
                h1 [] [ str "Pebble Statistics" ]
                p [] [ str $"Total pebbles: {total}" ]
                p [] [ str $"Custom pebbles: {custom}" ]
                h2 [] [ str "Category breakdown:" ]
                ul [] [
                    for (cat, count) in grouped do
                        li [] [ str $"{cat}: {count}" ]
                ]
            ]
        htmlView (html [] [ body [] [ content ] ]) next ctx

let webApp =
    choose [
        route "/api/all" >=> apiAllHandler
        route "/api/random" >=> apiRandomHandler
        routef "/category/%s" categoryHandler
        route "/" >=> homeHandler
        route "/stats" >=> statsHandler
    ]

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddGiraffe() |> ignore

    let app = builder.Build()
    app.UseGiraffe(webApp)
    app.Run()
    0