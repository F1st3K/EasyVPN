#!/bin/sh
# version: 0.7.0

echo "🚀 Starting init script..."


echo "🔌 Attempting to connect app..."

until curl -sv $API/health; do
  echo "⏳ Waiting for app to be ready..."
  sleep 2
done


echo "🔌 Attempting to connect to database..."

export PGPASSWORD="$(echo "$DB_CONNECTION_STRING" | sed -n 's|postgresql://[^:]*:\([^@]*\)@.*|\1|p')"
psql "$DB_CONNECTION_STRING" -c "\dt" > /dev/null 2>&1
if [ $? -ne 0 ]; then
  echo "❌ Failed to connect to the database!"
  exit 2
else
  echo "✅ Successfully connected to the database."
fi


echo "🟢 Initialization run..."

# Create security keeper
if [[ "$CREATE_SECURITY_KEEPER" != "NO" && "$CREATE_SECURITY_KEEPER" =~ ^[^:]+:[^:]+$ ]]; then
  LOGIN=$(echo "$CREATE_SECURITY_KEEPER" | cut -d: -f1)
  PASSWORD=$(echo "$CREATE_SECURITY_KEEPER" | cut -d: -f2)
  
  echo "📬 Creating [$LOGIN] user..."
  curl -v -X POST $API/auth/register \
    -H "Content-Type: application/json" \
    -d "{\"firstName\": \"Keeper\", \"lastName\": \"Initial\", \"login\": \"$LOGIN\", \"password\": \"$PASSWORD\" }"
  echo "" 
  echo "👑 Promoting [$LOGIN] to SecurityKeeper..."
  psql "$DB_CONNECTION_STRING" -c "UPDATE public.\"Users\" SET \"Roles\"='Administrator;PaymentReviewer;PageModerator;Client' WHERE \"Login\"='$LOGIN';"
fi


echo "✅ Init script finished!"