terraform {
  required_providers {
    hcloud = {
      source  = "hetznercloud/hcloud"
      version = "~> 1.45"
    }
  }
}

provider "hcloud" {
  token = var.hetzner_token
}

variable "hetzner_token" {
  description = "Hetzner API token"
  type        = string
  sensitive   = true
}

variable "public_key_path" {
  description = "Path to the Identity file"
  type        = string
}

resource "hcloud_ssh_key" "our_public_key" {
  name       = "ssh-public-key"
  public_key = file(var.public_key_path)
}

resource "hcloud_server" "my_server-1" {
  name        = "my-server-1"
  server_type = "cx22"
  image       = "debian-12"
  location    = "hel1"
  ssh_keys    = [hcloud_ssh_key.our_public_key.id]
}

output "server_address" {
  description = "The servers IPv4 address"
  value       = hcloud_server.my_server-1.ipv4_address
}
