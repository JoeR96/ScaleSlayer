# Stage 1: Build the React app
FROM node:18.20.2-alpine AS frontend-build

WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install 

# Copy the rest of the frontend files
COPY / ./
# Build the React app
RUN npm run dev
