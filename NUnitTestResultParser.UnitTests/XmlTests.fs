module XmlTests

open NUnit.Framework
open Xml
open FSharp.Data

type Json = JsonProvider<"config.json", EmbeddedResource="lib, lib.embedded.config.json">

type t = Json.Root

let load (path: string): t =
    Json.Load(path)

let testPath (config: t): string =
    config.TestPath

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.LoadXml() =
        let filePath = testPath(load("config.json"))
        let result = Xml.LoadXml(filePath)
        Assert.IsNotNull(result)