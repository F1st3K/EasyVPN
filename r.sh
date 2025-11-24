mapping() {
    local in="$1"
    local out=""
    local from=(v p n)
    local to=(z s v)

    for ((i=0; i<3; i++)); do
        c="${in:i:1}"
        if [[ "$c" =~ [A-Z] ]]; then
            out+="${to[$i]^^}"
        else
            out+="${to[$i]}"
        fi
    done

    printf "%s" "$out"
}

export -f mapping

find . -depth -print0 | while IFS= read -r -d '' path; do
    file="$path"

    # выделяем имя файла с помощью параметрического разборщика
    name="${file##*/}"
    dir="${file%/*}"

    # если файл в корне — фиксим dir
    [[ "$dir" == "$file" ]] && dir="."

    new="$name"

    while [[ "$new" =~ ([Vv][Pp][Nn]) ]]; do
        match="${BASH_REMATCH[1]}"
        repl="$(mapping "$match")"
        new="${new/$match/$repl}"
    done

    if [[ "$new" != "$name" ]]; then
        mv -- "$file" "$dir/$new"
    fi
done

