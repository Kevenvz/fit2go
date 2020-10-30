# Fit2Go automatic join lesson function

Azure Function made to automatically join lessons for the Fit2Go gym. Fit2Go uses [Sportivity](http://mijninsight.nl/) as it's platform.

***Note:** The sportivity platform is not safe as it sends your username and password in the body of every request.*

## How to run

Open the solution file at the root of the repository. Create a file named `config.json` inside the `Fit2go` project. See the `config.Example.json` file in the project for an example config.

Some things are hardcoded since I was too lazy to move them to the config.

Press F5 and enjoy.
