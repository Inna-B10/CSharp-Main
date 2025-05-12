import Image, { type ImageProps } from "next/image";
import { forwardRef } from "react";

/**
 * Thin wrapper around next/image for prefixing the src with the configured basePath
 */
const LocalImage = forwardRef<HTMLImageElement, ImageProps>((props, ref) => {
  const basePath = process.env.BUILD_TIME_BASE_PATH || "";

  const src =
    typeof props.src === "string" ? `${basePath}${props.src}` : props.src;

  // Return the Next.js Image component with all props forwarded
  return <Image {...props} src={src} ref={ref} />;
});

LocalImage.displayName = "LocalImage";

export default LocalImage;
