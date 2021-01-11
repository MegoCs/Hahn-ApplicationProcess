# `hahn-Applicants Sample`

![Main_CI](https://github.com/MegoCs/Hahn-ApplicatonProcess/workflows/Main_CI/badge.svg?branch=master)

This project is bootstrapped by [aurelia-cli](https://github.com/aurelia/cli).

For more information, go to https://aurelia.io/docs/cli/webpack

## Build the sample backend 

you can directly use `dotnet run` with the Web Api project location 

`Hahn.ApplicatonProcess.Application\Hahn.ApplicatonProcess.December2020.Web`

configure the right port you need to run. or just use the run default

update the ui project `config.json` file with the backend url. as the UI project is built as a separate service not hosted as a static files within the .net proj.

then

## Run the sample ui 

Run `npm i`, then `npm start` open `http://localhost:8080`

## CI pipline
i have also developed a CI pipeline within github to validate the proj with every pr and merge to master branch
