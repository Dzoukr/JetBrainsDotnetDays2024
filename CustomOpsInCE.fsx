type InputKind =
    | Text of placeholder:string option
    | Password of placeholder: string option

type Input = {
    Label: string option
    Kind : InputKind
    Age : int
}

type InputBuilder() =
    [<CustomOperation("text")>]
    member this.Text(io,?placeholder) =
        { io with Kind = Text placeholder }

    [<CustomOperation("password")>]
    member this.Password(io,?placeholder) =
        { io with Kind = Password placeholder }

    [<CustomOperation("label")>]
    member this.Label(io,label) =
        { io with Label = Some label }

    [<CustomOperation("marteenAge")>]
    member this.Label(io,age) =
        { io with Age = age }

    member t.Yield _ = {
        Label = None
        Kind = Text None
        Age = 18
    }

let input = InputBuilder()

input {
    text "Hi JetBrains! You IDE ROCKS!"
    label "So much Label, wow"
    marteenAge 25
}