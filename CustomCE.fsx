// Define the OptionBuilder type
type OptionBuilder() =
    // The Bind method is used to chain operations together
    member _.Bind(opt, f) =
        match opt with
        | Some value -> f(value)
        | None -> None

    // The Return method wraps a value in an Option
    member _.Return(value) = Some value

    // The Zero method provides a default value for when the expression yields no result
    member _.Zero() = None

// Create an instance of the OptionBuilder
let marteen = OptionBuilder()

// Example usage of the Option computation expression
let result = marteen {
    let! x = Some 10
    let! y = Some 20
    let! z = None // This will cause the entire computation to result in None
    return x + y + z
}