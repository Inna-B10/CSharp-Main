import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "A start of a game",
  description: "Bundled with other content",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased min-h-screen grid grid-rows-[minmax(0,auto)_1fr_minmax(0,auto)]`}
      >
        <header>
          <p className="text-center">Hello World</p>
        </header>

        <div>{children}</div>

        <footer>
          <p className="text-center">Made by yours truly!</p>
        </footer>
      </body>
    </html>
  );
}
