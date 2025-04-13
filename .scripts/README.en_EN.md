[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-gray
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-blue
[readme-en-url]: README.en_EN.md

## EasyVPN.Develop scripts

### This section contains scripts for development, they will be replenished during the life of the project:

> #### All scripts are usually executed from the root directory of the project - `./.scripts/<script_name>`.

1. `Dockerfile` - a container (not published anywhere) that contains all dependencies (supplemented) for a given project, in order to perform any manipulations without having all the tools locally.
    ```sh
    docker build -t dev-env ./.scripts/ && \
    docker run --rm -v "$PWD":/app dev-env <script> [args...]
    ```

2. `release.sh` (+ `set-global-version.sh`) - a script that "starts the process" of releasing a new version.
*(further: test the branch -> update the CHANGELOG -> commit and go to the script p.3)*
    ```sh
    ./.scripts/release.sh <x.x.x>
    ```

3. `publish.sh` - a script that "completes the process" of releasing a new version, publishes the branch on github. *(then: we do PullReuest of the release/x.x.x branch in main -> wait for the build testing to finish -> merge -> wait for the Workflow that creates the release -> merge main in develop)*
    ```sh
    ./.scripts/publish.sh
    ```