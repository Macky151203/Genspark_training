FROM node:latest

COPY . /usr/src/app
WORKDIR /usr/src/app
RUN npm install
EXPOSE 9000
CMD ["node", "index.js"]