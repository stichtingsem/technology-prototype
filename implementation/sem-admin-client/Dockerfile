FROM node:12.20.1
WORKDIR /app
ENV PATH /app/node_modules/:$PATH
COPY .npmrc .npmrc
COPY package.json package.json
COPY yarn.lock yarn.lock
RUN yarn
RUN yarn add react-scripts@3.4.1 -g
COPY . ./
CMD ["yarn", "start"]
