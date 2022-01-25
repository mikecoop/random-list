let randomList =
    RandomList.generateLengthMutable 10_000
    |> List.map string
    |> String.concat ","

printf $"{randomList}"