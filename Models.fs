module MindPebbles.Models

open System.Collections.Concurrent

type Thought = {
    Id: int
    Text: string
    Category: string
}

let thoughts = [
    { Id = 1; Text = "Sometimes silence says more than words."; Category = "calm" }
    { Id = 2; Text = "Even pebbles can cause ripples."; Category = "thought" }
    { Id = 3; Text = "Let go, not because you're weak, but because you're strong."; Category = "motivation" }
    { Id = 4; Text = "Stillness is not a pause, it's a purpose."; Category = "calm" }
    { Id = 5; Text = "The mind needs quiet to think loud."; Category = "thought" }
]

let private customBag = ConcurrentBag<string>()

let addCustom (text: string) =
    customBag.Add text

let allThoughts () =
    let customs =
        customBag
        |> Seq.toList
        |> List.mapi (fun i t -> { Id = 1000 + i; Text = t; Category = "custom" })
    customs @ thoughts

let getThoughtsByCategory (category: string) =
    allThoughts ()
    |> List.filter (fun t -> t.Category = category)

let stats () =
    let all = allThoughts()
    let total = List.length all
    let custom = List.filter (fun t -> t.Category = "custom") all |> List.length
    let grouped =
        all
        |> List.groupBy (fun t -> t.Category)
        |> List.map (fun (cat, list) -> (cat, List.length list))
    (total, custom, grouped)