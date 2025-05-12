import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: "standalone",
  basePath: process.env.BUILD_TIME_BASE_PATH,
};

export default nextConfig;
