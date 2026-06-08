# ============================
# BASE & DEV STAGE
# ============================
FROM node:22-alpine AS build-stage
WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install

# Copy project files
COPY . .

# Build the project (for production)
RUN npm run build

# ============================
# FINAL IMAGE (PRODUCTION)
# ============================
FROM nginx:alpine AS final
# Copy the built app to nginx
COPY --from=build-stage /app/dist /usr/share/nginx/html
# Expose port 80
EXPOSE 80
# Run nginx
CMD ["nginx", "-g", "daemon off;"]
