[![readme-ru-shield]][readme-ru-url]
[![readme-en-shield]][readme-en-url]

[readme-ru-shield]: https://img.shields.io/badge/ru-blue
[readme-ru-url]: README.md
[readme-en-shield]: https://img.shields.io/badge/en-gray
[readme-en-url]: README.en_EN.md


## EasyVPN.Develop scripts 

### В этом разделе находятся скрипты для разработки, в процессе жизни проекта они будут пополнятся:

> #### Все скрипты принято выполнять из корневого каталога проекта - `./.scripts/<имя_скрипта>`.

1. `Dockerfile` - контейнер (нигде не публикуется) в котором собраны все зависимости (дополняются) для данного проекта, что бы проводить какие либо манипуляции не имея всех инструментов локально. 
    ```sh
    docker build -t dev-env ./.scripts/ && \
        docker run --rm -v "$PWD":/app dev-env <script> [args...] 
    ```

2. `release.sh` (+ `set-global-version.sh`) - скрипт который "запускает процесс" релиза новой версии.
*(далее: тестим ветку -> обновляем  CHANGELOG -> коммитимся и переходим к скрипту п.3)*
    ```sh
    ./.scripts/release.sh <x.x.x>
    ```

3. `publish.sh` - скрипт который "завершает процесс" релиза новой версии, публикует ветку на github.
*(далее: делаем PullReuest ветки release/x.x.x в main -> ожидаем окончания тестирования сборки -> мерджимся -> дожидаемся Workflow создающий релиз -> мерджим main в develop)*
    ```sh
    ./.scripts/publish.sh
    ```