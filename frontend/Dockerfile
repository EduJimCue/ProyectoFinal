
FROM node:16-alpine AS build
RUN npm install -g http-server

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

EXPOSE 8081

CMD ["http-server", "dist"]
RUN npm install -g http-server
