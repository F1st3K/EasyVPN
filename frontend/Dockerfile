FROM node:lts-alpine as builder

WORKDIR /app
COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build


FROM nginx:alpine as runner

RUN rm -rf /usr/share/nginx/html/*

COPY --from=builder /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 3000

CMD ["/bin/sh", "-c", "envsubst < /usr/share/nginx/html/index.html > /usr/share/nginx/html/_index.html && mv /usr/share/nginx/html/_index.html /usr/share/nginx/html/index.html && exec nginx -g 'daemon off;'"]