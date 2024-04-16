FROM node:lts-alpine

WORKDIR /usr/local/src
COPY . ./

RUN npm install --legacy-peer-deps

CMD npm start