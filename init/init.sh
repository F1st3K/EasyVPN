#!/bin/sh
VERSION=0.7.2

echo ""
echo "🔧 Data Initialization Script"
echo "📜 Version: $VERSION"
echo "📅 Started at: $(date +'%Y-%m-%d %H:%M:%S')"
echo "--------------------------------------"
echo "🚀 Starting init script..."
echo ""


echo "🔌 Attempting to connect app..."

until curl -sv $API/health; do
  echo "⏳ Waiting for app to be ready..."
  sleep 2
done


echo "🔌 Attempting to connect to database..."

export PGPASSWORD="$DB_PASSWORD"
psql "$DB_CONNECTION_STRING" -c "\dt" > /dev/null 2>&1
if [ $? -ne 0 ]; then
  echo "❌ Failed to connect to the database!"
  exit 2
else
  echo "✅ Successfully connected to the database."
fi


echo "🟢 Initialization run at: $(date +'%Y-%m-%d %H:%M:%S')"


# Create security keeper
if [[ -n "$CREATE_SECURITY_KEEPER" && "$CREATE_SECURITY_KEEPER" != "NO" && "$CREATE_SECURITY_KEEPER" =~ ^[^:]+:[^:]+$ ]]; then
  printf "\n⚙️ Create security keeper:\n"
  LOGIN=$(echo "$CREATE_SECURITY_KEEPER" | cut -d: -f1)
  PASSWORD=$(echo "$CREATE_SECURITY_KEEPER" | cut -d: -f2)
  
  echo "📬 Creating [$LOGIN] user..."
  curl -v -X POST $API/auth/register \
    -H "Content-Type: application/json" \
    -d "{\"firstName\": \"Keeper\", \"lastName\": \"Initial\", \"login\": \"$LOGIN\", \"password\": \"$PASSWORD\" }"
  echo "" 
  echo "👑 Promoting [$LOGIN] to SecurityKeeper..."
  psql "$DB_CONNECTION_STRING" -c "UPDATE public.\"Users\" SET \"Roles\"='SecurityKeeper' WHERE \"Login\"='$LOGIN';"
fi


# Create dynamic-pages
if [[  -n "$CREATE_DYNAMIC_PAGES" && "$CREATE_DYNAMIC_PAGES" != "NO" ]]; then
  printf "\n⚙️ Create dynamic-pages:\n"
  TEMPLATE_TABLE="public.\"DynamicPages\""
  SEARCH_DIR="dynamic-pages"

  echo "📦 Build sql query..."
  TMP_SQL=$(mktemp)
  file -bi "$TMP_SQL"
  echo "INSERT INTO $TEMPLATE_TABLE (\"Route\", \"Title\", \"LastModified\", \"Created\", \"Content\") VALUES" > "$TMP_SQL"
  first_row=true
  find "$SEARCH_DIR" -type f -name "*.mdx" | while read -r FILE; do
      BASENAME=$(basename "$FILE")
      DIRNAME=$(dirname "$FILE")
      RELATIVE_PATH="${DIRNAME#"$SEARCH_DIR"}"
      RELATIVE_PATH="${RELATIVE_PATH#/}"

      TITLE="${BASENAME%%.*}"
      ROUTE_PART="${BASENAME#*.}"
      ROUTE_PART="${ROUTE_PART%.mdx}"

      if [ -z "$RELATIVE_PATH" ]; then
          FINAL_ROUTE="$ROUTE_PART"
      else
          FINAL_ROUTE="$RELATIVE_PATH/$ROUTE_PART"
      fi

      MODIFIED=$(stat -c %y "$FILE")
      CREATED="$MODIFIED"

      CONTENT=$(cat "$FILE")
      BASE64_CONTENT=$(echo "$CONTENT" | base64 | tr -d '\n')

      VALUE_ROW="('$FINAL_ROUTE', '$TITLE', '$MODIFIED', '$CREATED', '$BASE64_CONTENT')"

      if $first_row; then
          printf "\n  %s" "$VALUE_ROW" >> "$TMP_SQL"
          first_row=false
      else
          printf ",\n  %s" "$VALUE_ROW" >> "$TMP_SQL"
      fi
      echo "🧱 Add $FINAL_ROUTE - $TITLE"
  done
  printf ";\n" >> "$TMP_SQL"

  echo "📨 Run sql query..."
  file -bi "$TMP_SQL"
  psql "$DB_CONNECTION_STRING" -f "$TMP_SQL"
  rm "$TMP_SQL" 
fi



echo "--------------------------------------"
echo "✅ Init script $VERSION finished!"
echo "⏱ Finished at: $(date +'%Y-%m-%d %H:%M:%S')"