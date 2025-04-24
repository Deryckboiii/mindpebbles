module MindPebbles.Views

open Giraffe.ViewEngine

let layout (quote: string) (bgColor: string) =
    html [] [
        head [] [
            title []      [ str "MindPebbles" ]
            meta [ _charset "UTF-8" ]
            link [ _rel "preconnect"; _href "https://fonts.googleapis.com" ]
            link [ _rel "stylesheet"; _href "https://fonts.googleapis.com/css2?family=Quicksand:wght@400;600&display=swap" ]
            style [] [
                rawText $"""
                    body {{
                        font-family: 'Quicksand', sans-serif;
                        background: {bgColor};
                        color: #333;
                        display: flex;
                        flex-direction: column;
                        align-items: center;
                        justify-content: center;
                        height: 100vh;
                        margin: 0;
                        transition: background 0.3s, color 0.3s;
                    }}

                    .dark-mode {{
                        background: #121212 !important;
                        color: #f0f0f0 !important;
                    }}

                    .dark-mode .quote {{
                        background: #1e1e1e;
                        color: #f0f0f0;
                        border-left-color: #bb86fc;
                    }}

                    .quote {{
                        font-size: 1.8rem;
                        font-weight: 600;
                        margin-bottom: 2rem;
                        padding: 1rem 2rem;
                        border-left: 4px solid #6c63ff;
                        background: #fff;
                        box-shadow: 0 4px 8px rgba(0,0,0,0.05);
                        border-radius: 8px;
                        max-width: 600px;
                    }}

                    button {{
                        padding: 0.6rem 2rem;
                        font-size: 1rem;
                        font-weight: bold;
                        border: none;
                        border-radius: 25px;
                        background: #6c63ff;
                        color: white;
                        cursor: pointer;
                        transition: background 0.3s ease;
                        margin: 0.5rem;
                    }}

                    button:hover {{
                        background: #574b90;
                    }}

                    #toggle-dark {{
                        position: absolute;
                        top: 1rem;
                        right: 1rem;
                        background: transparent;
                        border: 2px solid #6c63ff;
                        color: #6c63ff;
                    }}

                    #toggle-dark:hover {{
                        background: #6c63ff;
                        color: white;
                    }}
                """
            ]
            script [] [
                rawText """
                    function applyDarkMode(enabled) {
                        if (enabled) {
                            document.body.classList.add('dark-mode');
                        } else {
                            document.body.classList.remove('dark-mode');
                        }
                    }

                    function toggleDarkMode() {
                        const enabled = document.body.classList.toggle('dark-mode');
                        localStorage.setItem('darkMode', enabled ? 'true' : 'false');
                    }

                    window.onload = function() {
                        // dark mode restore
                        const saved = localStorage.getItem('darkMode');
                        if (saved === 'true') { applyDarkMode(true); }

                        // favorite pebble restore
                        const fav = localStorage.getItem('favoritePebble');
                        if (fav) {
                            document.getElementById('favorite-pebble').textContent = "Your Favorite Pebble: " + fav;
                        }
                    };

                    function saveFavoritePebble(quote) {
                        localStorage.setItem('favoritePebble', quote);
                        document.getElementById('favorite-pebble').textContent = "Your Favorite Pebble: " + quote;
                    }
                """
            ]
        ]
        body [] [
            div [ _class "quote" ] [ str quote ]
            form [ _method "GET"; _action "/" ] [
                button [] [ str "Give me another pebble" ]
            ]
            button [ _id "toggle-dark";   _onclick "toggleDarkMode()"             ] [ str "Toggle Dark Mode"             ]
            button [ _id "save-favorite"; _onclick ("saveFavoritePebble('" + quote.Replace("'", "\\'") + "')") ]
                   [ str "Save as Favorite Pebble" ]
            div    [ _id "favorite-pebble" ] [ str "Your Favorite Pebble: Not Set" ]
        ]
    ]

let homeView (quote: string) (bgColor: string) =
    layout quote bgColor